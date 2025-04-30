export class CreditCardsInterop {
    constructor() {
        this._styleMap = new Map();
        this.cleanups = new Map();
    }

    create(container, card, elementId) {
        this.initializeCardStyling(card);

        this.cleanups.set(elementId, { container, card });
        this.createObserver(elementId);
    }

    initializeCardStyling(card) {
        const cardTypes = ['visa', 'mastercard', 'amex', 'discover'];
        const matched = cardTypes.find(type => card.classList.contains(`card--${type}`));
        if (matched) {
            if (this._styleMap.has(matched)) {
                this.applyMappedStyle(card, matched);
            } else {
                this.applyCardStyle(card, matched);
            }
        }
    }

    updateCardStyle(card, style) {
        const background = card.querySelector('.card__background');
        const pattern = card.querySelector('.card__pattern');

        if (background) {
            background.style.background = style.gradient || style.backgroundColor || '';
        }

        if (pattern && style.pattern && style.pattern !== 'none') {
            pattern.style.backgroundImage = style.pattern;
            pattern.style.opacity = '0.1';
        }

        // Common logo logic for both front and back
        const brandType = (style.type || 'unknown').toLowerCase();
        const iconStyle = style.iconStyle || 'logo';
        const logoUrl = `https://cdn.jsdelivr.net/gh/aaronfagan/svg-credit-card-payment-icons/${iconStyle}/${brandType}.svg`;
        const logoPosition = style.logoPosition || 'center';

        // Update front logo
        const brandFront = card.querySelector('.card__brand--front');
        if (brandFront) {
            brandFront.style.backgroundImage = `url(${logoUrl})`;
            brandFront.style.backgroundSize = 'contain';
            brandFront.style.backgroundRepeat = 'no-repeat';
            brandFront.style.backgroundPosition = logoPosition;
        }

        // Update back logo
        const brandBack = card.querySelector('.card__brand--back');
        if (brandBack) {
            brandBack.style.backgroundImage = `url(${logoUrl})`;
            brandBack.style.backgroundSize = 'contain';
            brandBack.style.backgroundRepeat = 'no-repeat';
            brandBack.style.backgroundPosition = logoPosition;
        }

        this.updateCardFeatures(card, style);
    }

    updateCardFeatures(card, style) {
    }

    applyCardStyle(card, type) {
        const background = card.querySelector('.card__background');
        if (background) background.style.background = this.getDefaultGradient(type);

        const brand = card.querySelector('.card__brand');
        if (brand) {
            const iconStyle = 'logo';
            const brandType = type.toLowerCase();
            brand.style.backgroundImage = `url(https://cdn.jsdelivr.net/gh/aaronfagan/svg-credit-card-payment-icons/${iconStyle}/${brandType}.svg)`;
            brand.style.backgroundSize = 'contain';
            brand.style.backgroundRepeat = 'no-repeat';
            brand.style.backgroundPosition = 'center';
        }
    }

    getDefaultGradient(type) {
        const gradients = {
            visa: 'linear-gradient(135deg, #1a1f71, #f7b600)',
            mastercard: 'linear-gradient(135deg, #f46b20, #eea849)',
            amex: 'linear-gradient(135deg, #007cc3, #003087)',
            discover: 'linear-gradient(135deg, #ff6000, #ff8c00)'
        };
        return gradients[type] || gradients.visa;
    }

    createObserver(elementId) {
        const target = document.getElementById(elementId);
        if (!target || !target.parentNode || !this.cleanups.has(elementId)) return;

        const observer = new MutationObserver((mutations) => {
            const targetRemoved = mutations.some(mutation =>
                Array.from(mutation.removedNodes).includes(target)
            );

            if (targetRemoved) {
                this.dispose(elementId);
                observer.disconnect();
            }
        });

        observer.observe(target.parentNode, { childList: true });

        const entry = this.cleanups.get(elementId);
        if (entry) {
            entry.observer = observer;
            this.cleanups.set(elementId, entry);
        }
    }

    dispose(elementId) {
        const entry = this.cleanups.get(elementId);
        if (entry) {
            if (entry.observer) entry.observer.disconnect();
            this.cleanups.delete(elementId);
        }
    }

    setCardStyleMapping(type, style) {
        this._styleMap.set(type.toLowerCase(), style);
    }

    applyMappedStyle(card, type) {
        const style = this._styleMap.get(type.toLowerCase());
        if (style) this.updateCardStyle(card, style);
    }
}

window.CreditCardsInterop = new CreditCardsInterop();