const styleMap = new Map();
const cleanups = new Map();

function applyMappedStyle(card, type) {
    const style = styleMap.get(type.toLowerCase());
    if (style) updateCardStyle(card, style);
}

function applyDefaultStyle(card, type) {
    const style = {
        gradient: getDefaultGradient(type),
        type
    };
    updateCardStyle(card, style);
}

function setBackground(card, style) {
    const background = card.querySelector('.card__background');
    const pattern = card.querySelector('.card__pattern');

    if (background) {
        background.style.background = style.gradient || style.backgroundColor || '';
    }

    if (pattern && style.pattern && style.pattern !== 'none') {
        pattern.style.backgroundImage = style.pattern;
        pattern.style.opacity = '0.1';
    }
}

function setLogo(card, style) {
    const brandType = (style.type || 'unknown').toLowerCase();
    const iconStyle = style.iconStyle || 'logo';
    const position = style.logoPosition || 'center';
    const url = `https://cdn.jsdelivr.net/gh/aaronfagan/svg-credit-card-payment-icons/${iconStyle}/${brandType}.svg`;

    for (const selector of ['.card__brand--front', '.card__brand--back']) {
        const el = card.querySelector(selector);
        if (el) {
            el.style.backgroundImage = `url(${url})`;
            el.style.backgroundSize = 'contain';
            el.style.backgroundRepeat = 'no-repeat';
            el.style.backgroundPosition = position;
        }
    }
}

function getDefaultGradient(type) {
    const gradients = {
        visa: 'linear-gradient(135deg, #1a1f71, #f7b600)',
        mastercard: 'linear-gradient(135deg, #f46b20, #eea849)',
        amex: 'linear-gradient(135deg, #007cc3, #003087)',
        discover: 'linear-gradient(135deg, #ff6000, #ff8c00)'
    };
    return gradients[type.toLowerCase()] || gradients.visa;
}

function createObserverForElement(elementId) {
    const target = document.getElementById(elementId);
    const entry = cleanups.get(elementId);
    if (!target || !target.parentNode || !entry) return;

    const observer = new MutationObserver((mutations) => {
        const removed = mutations.some(m => [...m.removedNodes].includes(target));
        if (removed) {
            dispose(elementId);
            observer.disconnect();
        }
    });

    observer.observe(target.parentNode, { childList: true });
    entry.observer = observer;
    cleanups.set(elementId, entry);
}

export function create(container, card, elementId) {
    initializeCardStyling(card);
    cleanups.set(elementId, { container, card });
    createObserverForElement(elementId);
}

function initializeCardStyling(card) {
    const knownTypes = ['visa', 'mastercard', 'amex', 'discover'];
    const matchedType = knownTypes.find(type => card.classList.contains(`card--${type}`));
    if (!matchedType) return;

    if (styleMap.has(matchedType)) {
        applyMappedStyle(card, matchedType);
    } else {
        applyDefaultStyle(card, matchedType);
    }
}

export function updateCardStyle(card, style) {
    setBackground(card, style);
    setLogo(card, style);
}

export function dispose(elementId) {
    const entry = cleanups.get(elementId);
    if (entry?.observer) entry.observer.disconnect();
    cleanups.delete(elementId);
}
