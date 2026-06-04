using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.CreditCards.Abstract;
using Soenneker.Blazor.CreditCards.Dtos;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Blazor.CreditCards;

///<inheritdoc cref="ICreditCardsInterop"/>
public sealed class CreditCardsInterop : ICreditCardsInterop
{
    private const string _modulePath = "_content/Soenneker.Blazor.CreditCards/js/creditcardsinterop.js";

    private readonly IResourceLoader _resourceLoader;
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    public CreditCardsInterop(IResourceLoader resourceLoader, IModuleImportUtil moduleImportUtil)
    {
        _resourceLoader = resourceLoader;
        _moduleImportUtil = moduleImportUtil;
        _initializer = new AsyncInitializer(InitializeAssets);
    }

    private async ValueTask InitializeAssets(CancellationToken token)
    {
        await _resourceLoader.LoadStyle("_content/Soenneker.Blazor.CreditCards/css/creditcards.css", cancellationToken: token);

        _ = await _moduleImportUtil.GetContentModuleReference(_modulePath, token);
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _initializer.Init(linked);
    }

    public async ValueTask Create(ElementReference container, ElementReference card, string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            await _initializer.Init(linked);

            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("create", linked, container, card, id);
        }
    }

    public async ValueTask UpdateCardStyle(ElementReference card, CardStyle style, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("updateCardStyle", linked, card, style);
        }
    }

    public async ValueTask Destroy(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("dispose", linked, id);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _moduleImportUtil.DisposeContentModule(_modulePath);
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
