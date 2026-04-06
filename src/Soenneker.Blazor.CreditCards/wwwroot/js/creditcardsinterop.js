const interop = (() => {
    const instance = {};
    instance.create = function(container, card, elementId) {
        this.initializeCardStyling(card);
        this._cleanups.set(elementId, { container, card });
        this._createObserver(elementId);
    };

    instance.initializeCardStyling = function(card) {
        const knownTypes = ['visa', 'mastercard', 'amex', 'discover'];
        const matchedType = knownTypes.find(type => card.classList.contains(`card--${type}`));
        if (!matchedType) return;

        if (this._styleMap.has(matchedType)) {
            this._applyMappedStyle(card, matchedType);
        } else {
            this._applyDefaultStyle(card, matchedType);
        }
    };

    instance.updateCardStyle = function(card, style) {
        this._setBackground(card, style);
        this._setLogo(card, style);
        this.updateCardFeatures(card, style);
    };

    instance.updateCardFeatures = function(card, style) {
        // Reserved for extending (e.g., adding holograms, chip types, etc.)
    };

    instance.setCardStyleMapping = function(type, style) {
        this._styleMap.set(type.toLowerCase(), style);
    };

    instance.dispose = function(elementId) {
        const entry = this._cleanups.get(elementId);
        if (entry?.observer) entry.observer.disconnect();
        this._cleanups.delete(elementId);
    };

    instance._applyMappedStyle = function(card, type) {
        const style = this._styleMap.get(type.toLowerCase());
        if (style) this.updateCardStyle(card, style);
    };

    instance._applyDefaultStyle = function(card, type) {
        const style = {
            gradient: this._getDefaultGradient(type),
            type
        };
        this.updateCardStyle(card, style);
    };

    instance._setBackground = function(card, style) {
        const background = card.querySelector('.card__background');
        const pattern = card.querySelector('.card__pattern');

        if (background) {
            background.style.background = style.gradient || style.backgroundColor || '';
        }

        if (pattern && style.pattern && style.pattern !== 'none') {
            pattern.style.backgroundImage = style.pattern;
            pattern.style.opacity = '0.1';
        }
    };

    instance._setLogo = function(card, style) {
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
    };

    instance._getDefaultGradient = function(type) {
        const gradients = {
            visa: 'linear-gradient(135deg, #1a1f71, #f7b600)',
            mastercard: 'linear-gradient(135deg, #f46b20, #eea849)',
            amex: 'linear-gradient(135deg, #007cc3, #003087)',
            discover: 'linear-gradient(135deg, #ff6000, #ff8c00)'
        };
        return gradients[type.toLowerCase()] || gradients.visa;
    };

    instance._createObserver = function(elementId) {
        const target = document.getElementById(elementId);
        const entry = this._cleanups.get(elementId);
        if (!target || !target.parentNode || !entry) return;

        const observer = new MutationObserver((mutations) => {
            const removed = mutations.some(m => [...m.removedNodes].includes(target));
            if (removed) {
                this.dispose(elementId);
                observer.disconnect();
            }
        });

        observer.observe(target.parentNode, { childList: true });
        entry.observer = observer;
        this._cleanups.set(elementId, entry);
    };

        instance._styleMap = new Map();
        instance._cleanups = new Map();
    

    return instance;
})();
export function create(container, card, elementId) {
    return interop.create(container, card, elementId);
}

export function updateCardStyle(card, style) {
    return interop.updateCardStyle(card, style);
}

export function dispose(elementId) {
    return interop.dispose(elementId);
}
