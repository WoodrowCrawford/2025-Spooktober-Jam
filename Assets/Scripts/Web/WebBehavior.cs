using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections;
using System;
using Naninovel.Commands;
using Naninovel;

// WebBehavior: manages in-game websites/pages and saves/restores the currently-open site/page
public class WebBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI Camera")]
    [SerializeField] private Camera _uiCamera;

    [Header("Website Parameters")]
    [SerializeField] private GameObject _currentWebsite;
    public GameObject previousWebsite;
    [SerializeField] private GameObject _savedWebsite;

    [Header("Web Sites")]
    [SerializeField] private GameObject _veryThingWebsite;
    [SerializeField] private GameObject _clickWebsite;
    [SerializeField] private GameObject _chatterWebsite;
    [SerializeField] private GameObject _cloverWebsite;
    [SerializeField] private GameObject _salvaVeritateWebsite;

    [Header("Webpage Pages")]
    [SerializeField] private GameObject _currentPage;
    [SerializeField] private GameObject _previousPage;
    [SerializeField] private GameObject _savedPage;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _favoritesButton;

    [Header("Scroll Parameters")]
    [SerializeField] private GameObject _scrollView;
    [SerializeField] private Scrollbar _scrollBar;

    [Header("Bools")]
    [SerializeField] private bool _isClicked = false;
    public static bool WebIsOpen = false;

    private IStateManager _stateManager;

    [Serializable]
    private class WebState
    {
        public string WebsiteName;
        public string PageName;
    }

    private bool _handlersRegistered = false;

    void OnEnable()
    {
        Engine.OnInitializationFinished += HandleInitializationFinished;
        if (Engine.Initialized) HandleInitializationFinished();

        DesktopManager.OnVerificationPopUpShown += UnblockWebpageInteraction;
        DesktopManager.OnVerificationPopUpClosed += BlockWebpageInteraction;

        OnEnableWebInteractionCommand.OnEnableWebInteraction += BlockWebpageInteraction;
        OnDisableWebInteractionCommand.OnDisableWebInteraction += UnblockWebpageInteraction;

        WebsiteEvents.OnWebsiteChange += ChangeWebsitePage;
        OnOpenNewsCommand.OnOpenNews += ChangeWebpageToSalvaVeritate;
        OnOpenCloverCommand.OnOpenClover += ChangeWebpageToClover;
        OnOpenChatterCommand.OnOpenChatter += ChangeWebpageToChatter;
        OnOpenClickCommand.OnOpenClick += ChangeWebpageToClick;
        OnOpenVeryThingCommand.OnOpenVeryThing += ChangeWebpageToVeryThing;

        var cam = GameObject.Find("UICamera");
        if (cam) _uiCamera = cam.GetComponent<Camera>();

        WebIsOpen = true;
    }

    void OnDisable()
    {
        Engine.OnInitializationFinished -= HandleInitializationFinished;
        DesktopManager.OnVerificationPopUpShown -= UnblockWebpageInteraction;
        DesktopManager.OnVerificationPopUpClosed -= BlockWebpageInteraction;

        OnEnableWebInteractionCommand.OnEnableWebInteraction -= BlockWebpageInteraction;
        OnDisableWebInteractionCommand.OnDisableWebInteraction -= UnblockWebpageInteraction;

        WebsiteEvents.OnWebsiteChange -= ChangeWebsitePage;
        OnOpenNewsCommand.OnOpenNews -= ChangeWebpageToSalvaVeritate;
        OnOpenCloverCommand.OnOpenClover -= ChangeWebpageToClover;
        OnOpenChatterCommand.OnOpenChatter -= ChangeWebpageToChatter;
        OnOpenClickCommand.OnOpenClick -= ChangeWebpageToClick;
        OnOpenVeryThingCommand.OnOpenVeryThing -= ChangeWebpageToVeryThing;

        _uiCamera = null;
        WebIsOpen = false;

        if (_stateManager != null && _handlersRegistered)
        {
            _stateManager.OnGameSaveStarted -= OnStateManager_GameSaveStarted;
            _stateManager.OnGameLoadFinished -= OnStateManager_GameLoadFinished;
            _stateManager.RemoveOnGameSerializeTask(SerializeState);
            _stateManager.RemoveOnGameDeserializeTask(DeserializeStateAsync);
            _handlersRegistered = false;
        }

    _stateManager = null;
    }

    public void OnBeginDrag(PointerEventData eventData) { }
    public void OnDrag(PointerEventData eventData) { }
    public void OnEndDrag(PointerEventData eventData) { }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == _exitButton)
        {
            this.gameObject.SetActive(false);
            return;
        }

        if (eventData.pointerCurrentRaycast.gameObject == _backButton)
        {
            if (_previousPage != null)
            {
                ChangeWebsitePage(_previousPage);
                Debug.Log("Back Button Clicked");
            }
            else Debug.Log("No previous page to go back to");

            return;
        }

        if (eventData.pointerCurrentRaycast.gameObject == _favoritesButton)
        {
            Debug.Log("Favorites Button Clicked");
            WebsiteEvents.RaisePlayerClickedFavoritesButton();
            return;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isClicked = false;
    }

    void Start()
    {
        ChangeWebsite(_salvaVeritateWebsite);
        if (_currentWebsite != null && _currentWebsite.transform.childCount > 0)
            _currentPage = _currentWebsite.transform.GetChild(0).gameObject;
    }

    public void HandleInitializationFinished()
    {
        if (_handlersRegistered) return;

        _stateManager = Engine.GetService<IStateManager>();
        if (_stateManager == null) return;

        _stateManager.AddOnGameSerializeTask(SerializeState);
        _stateManager.AddOnGameDeserializeTask(DeserializeStateAsync);
        _stateManager.OnGameSaveStarted += OnStateManager_GameSaveStarted;
        _stateManager.OnGameLoadFinished += OnStateManager_GameLoadFinished;

        _handlersRegistered = true;
    }

    private void OnStateManager_GameSaveStarted(GameSaveLoadArgs args) => UpdateCurrentWebpageOnSave();
    private void OnStateManager_GameLoadFinished(GameSaveLoadArgs args) => GetCurrentWebpageOnLoad();

    private void SerializeState(GameStateMap stateMap)
    {
        try
        {
            var state = new WebState { WebsiteName = _currentWebsite?.name, PageName = _currentPage?.name };
            stateMap.SetState(state, nameof(WebBehavior));
            Debug.Log($"[WebBehavior] Serialized web state: {state.WebsiteName} - {state.PageName}");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[WebBehavior] SerializeState failed: {e}");
        }
    }

    private UniTask DeserializeStateAsync(GameStateMap stateMap)
    {
        try
        {
            var state = stateMap.GetState<WebState>(nameof(WebBehavior));
            if (state == null) return UniTask.CompletedTask;

            Debug.Log($"[WebBehavior] Deserializing web state: {state.WebsiteName} - {state.PageName}");

            var website = FindWebsiteByName(state.WebsiteName);
            if (website != null)
            {
                // Make sure the website GameObject is active in the scene before switching
                try { website.SetActive(true); } catch (Exception) { }
                    _savedWebsite = website; // Remember resolved website and apply it
                    EnsurePanelVisible();
                    ChangeWebsite(website);
            }

            if (!string.IsNullOrEmpty(state.PageName))
            {
                var page = FindPageByName(website ?? _currentWebsite, state.PageName);
                if (page != null)
                {
                    try { page.SetActive(true); } catch (Exception) { }
                        _savedPage = page; // Remember resolved page and apply it
                        EnsurePanelVisible();
                        ChangeWebsitePage(page);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[WebBehavior] DeserializeStateAsync failed: {e}");
        }

        return UniTask.CompletedTask;
    }

    private GameObject FindWebsiteByName(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;
        var candidates = new GameObject[] { _veryThingWebsite, _clickWebsite, _chatterWebsite, _cloverWebsite, _salvaVeritateWebsite };
        foreach (var go in candidates)
            if (go != null && go.name == name) return go;
        return null;
    }

    private GameObject FindPageByName(GameObject website, string pageName)
    {
        if (website == null || string.IsNullOrEmpty(pageName)) return null;
        for (int i = 0; i < website.transform.childCount; i++)
        {
            var child = website.transform.GetChild(i).gameObject;
            if (child != null && child.name == pageName) return child;
        }
        return null;
    }

    // Ensure the web panel (this GameObject) and its parent Canvas/CanvasGroup are visible and interactive.
    private void EnsurePanelVisible()
    {
        try
        {
            // Activate the root web panel
            if (!gameObject.activeInHierarchy) gameObject.SetActive(true);

            // Try enabling CanvasGroup if present
            var cg = GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 1f;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }

            // Ensure any parent Canvas is enabled
            var canvas = GetComponentInParent<Canvas>();
            if (canvas != null) canvas.enabled = true;
        }
        catch (Exception) { /* best-effort */ }
    }

    public void UpdateCurrentWebpageOnSave()
    {
        if (_currentWebsite == null || _currentPage == null) return;
        Debug.Log("Saving current webpage state: " + _currentWebsite.name + " - " + _currentPage.name);
        _savedWebsite = _currentWebsite;
        _savedPage = _currentPage;
    }

    public void GetCurrentWebpageOnLoad()
    {
        // Expose a simple public restore entrypoint by delegating to RestoreSavedWebsite
        RestoreSavedWebsite();
    }

    /// <summary>
    /// Restores the saved website and page (if any) by making sure the panel and saved GameObjects
    /// are active and then applying them. Also starts a retrying coroutine to override any
    /// post-load UI hiding performed by Naninovel.
    /// Can be called externally (for example, by other systems after a load) to force restore.
    /// </summary>
    public void RestoreSavedWebsite()
    {
        if (_savedWebsite != null)
        {
            // Ensure saved website/page GameObjects are enabled in case they were disabled in the scene
            try
            {
                _savedWebsite.SetActive(true);
                if (_savedPage != null) _savedPage.SetActive(true);
            }
            catch (Exception) { /* ignore if references lost */ }

            // Ensure the whole web panel is visible and interactive
            EnsurePanelVisible();

            ChangeWebsite(_savedWebsite);
            ChangeWebsitePage(_savedPage);

            // Start coroutine to reapply saved website/page after a frame (Naninovel may hide UIs during load)
            StartCoroutine(ReapplySavedWebsiteAfterLoad());
            return;
        }

        Debug.Log("No saved website fields set; deferring to serialized game state (if any)");
    }

    private IEnumerator ReapplySavedWebsiteAfterLoad()
    {
        // Try multiple times (frames + short waits) to survive Naninovel post-load UI hiding.
        const int attempts = 5;
        for (int i = 0; i < attempts; i++)
        {
            // wait a frame and a small real-time delay to allow engine/UI to finish
            yield return null;
            yield return new WaitForSeconds(0.03f);

            try
            {
                if (_savedWebsite != null)
                {
                    EnsurePanelVisible();
                    _savedWebsite.SetActive(true);
                    ChangeWebsite(_savedWebsite);
                }

                if (_savedPage != null)
                {
                    _savedPage.SetActive(true);
                    ChangeWebsitePage(_savedPage);
                }

                Debug.Log($"[WebBehavior] ReapplySavedWebsiteAfterLoad attempt {i + 1}/{attempts} applied.");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[WebBehavior] ReapplySavedWebsiteAfterLoad attempt {i + 1} failed: {e}");
            }
        }
    }

    public void EnableFavoritesButton()
    {
        _favoritesButton.SetActive(true);
    }

    public void ChangeWebsitePage(GameObject newWebsitePage)
    {
        WebIsOpen = true;

        if (newWebsitePage == null)
        {
            Debug.LogWarning("ChangeWebsitePage called with null newWebsitePage");
            return;
        }

        Debug.Log("Changing website page to: " + newWebsitePage.name);

        _previousPage = _currentPage;
        _currentPage = newWebsitePage;

        if (_currentWebsite != null)
        {
            foreach (Transform child in _currentWebsite.transform)
                child.gameObject.SetActive(false);
        }
        if (_scrollBar != null) _scrollBar.value = 1f;
        if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().enabled = true;
        if (_scrollBar != null) _scrollBar.interactable = true;

        newWebsitePage.SetActive(true);

        if (_currentPage != null && (_currentPage.name == "SalvaVeritate-EndlessFunPage" || _currentPage.name == "SalvaVeritate-ChristianSchoolPage" || _currentPage.name == "SalvaVeritate-AIChangingLifePage"))
        {
            if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 0f;
            if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().enabled = false;
            if (_scrollBar != null) _scrollBar.interactable = false;
        }
        else
        {
            if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f;
            if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().enabled = true;
            if (_scrollBar != null) _scrollBar.interactable = true;
        }
    }

    public void ChangeWebsite(GameObject newWebsite)
    {
        WebIsOpen = true;

        if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f;
        if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().enabled = true;
        if (_scrollBar != null) _scrollBar.interactable = true;

        if (newWebsite == null)
        {
            Debug.LogWarning("ChangeWebsite called with null newWebsite");
            return;
        }

        previousWebsite = _currentWebsite;

        if (_currentWebsite != null)
        {
            foreach (Transform child in _currentWebsite.transform)
                child.gameObject.SetActive(false);
        }

        _currentWebsite = newWebsite;
        _currentWebsite.SetActive(true);

        if (_currentWebsite.transform.childCount > 0)
        {
            for (int i = 0; i < _currentWebsite.transform.childCount; i++)
            {
                var child = _currentWebsite.transform.GetChild(i).gameObject;
                child.SetActive(i == 0);
            }
        }

        if (_scrollView != null)
        {
            var scrollRect = _scrollView.GetComponent<ScrollRect>();
            if (scrollRect != null)
            {
                var rect = _currentWebsite.GetComponent<RectTransform>();
                if (rect != null) scrollRect.content = rect;
            }
        }

        if (_scrollBar != null) _scrollBar.value = 1f;

        if (previousWebsite != null)
            previousWebsite.SetActive(false);
    }

    public void ChangeWebpageToSalvaVeritate()
    {
        if (_currentWebsite == _salvaVeritateWebsite) return;
        ChangeWebsite(_salvaVeritateWebsite);
        if (_currentWebsite != null && _currentWebsite.transform.childCount > 0) _currentPage = _currentWebsite.transform.GetChild(0).gameObject;
        if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f;
        if (_scrollBar != null) _scrollBar.value = 1f;
    }

    public void ChangeWebpageToChatter() { if (_currentWebsite == _chatterWebsite) return; ChangeWebsite(_chatterWebsite); if (_currentWebsite != null && _currentWebsite.transform.childCount > 0) _currentPage = _currentWebsite.transform.GetChild(0).gameObject; if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f; if (_scrollBar != null) _scrollBar.value = 1f; }
    public void ChangeWebpageToClover() { if (_currentWebsite == _cloverWebsite) return; ChangeWebsite(_cloverWebsite); if (_currentWebsite != null && _currentWebsite.transform.childCount > 0) _currentPage = _currentWebsite.transform.GetChild(0).gameObject; if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f; if (_scrollBar != null) _scrollBar.value = 1f; }
    public void ChangeWebpageToVeryThing() { if (_currentWebsite == _veryThingWebsite) return; ChangeWebsite(_veryThingWebsite); if (_currentWebsite != null && _currentWebsite.transform.childCount > 0) _currentPage = _currentWebsite.transform.GetChild(0).gameObject; if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f; if (_scrollBar != null) _scrollBar.value = 1f; }
    public void ChangeWebpageToClick() { if (_currentWebsite == _clickWebsite) return; ChangeWebsite(_clickWebsite); if (_currentWebsite != null && _currentWebsite.transform.childCount > 0) _currentPage = _currentWebsite.transform.GetChild(0).gameObject; if (_scrollView != null) _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f; if (_scrollBar != null) _scrollBar.value = 1f; }

    public void BlockWebpageInteraction() { this.GetComponent<CanvasGroup>().blocksRaycasts = true; }
    public void UnblockWebpageInteraction() { this.GetComponent<CanvasGroup>().blocksRaycasts = false; }

}
