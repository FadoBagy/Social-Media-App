function renderOptionsPanel() {
    const optionsPanelEl = document.querySelector('.options-panel');
    const bodyEl = document.body;

    let panelIsShown = false;
    let hasScrollbar = window.innerWidth > document.documentElement.clientWidth;

    document.querySelector('.options img').addEventListener('click', () => {
        ShowOptionsPanel();
    });

    function ShowOptionsPanel() {
        bodyEl.style.overflow = 'hidden';
        if (hasScrollbar) {
            bodyEl.style.paddingRight = '13px';
        }
        optionsPanelEl.style.display = 'flex';
        optionsPanelEl.style.top = `${window.scrollY}px`;
        optionsPanelEl.classList.toggle('visible');

        optionsPanelEl.addEventListener('click', OptionsPaneClickHandler);
        panelIsShown = true;
    };

    function HideOptionsPanel() {
        optionsPanelEl.classList.toggle('visible');
        setTimeout(() => {
            bodyEl.style.overflow = 'auto';
            if (hasScrollbar) {
                bodyEl.style.paddingRight = '0';
            }
            optionsPanelEl.style.display = 'none';
        }, 150);

        optionsPanelEl.removeEventListener('click', OptionsPaneClickHandler);
        panelIsShown = false;
    };

    function OptionsPaneClickHandler(e) {
        let clickedEl = e.target;
        CheckIfClickedOutside(clickedEl);
    };

    function CheckIfClickedOutside(clickedEl) {
        if (clickedEl.classList.contains('options-panel')) {
            HideOptionsPanel();
        }
    }
}

export { renderOptionsPanel };