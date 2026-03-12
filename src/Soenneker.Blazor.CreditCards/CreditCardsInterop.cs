using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.CreditCards.Abstract;
using Soenneker.Blazor.CreditCards.Dtos;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Blazor.CreditCards;

///<inheritdoc cref="ICreditCardsInterop"/>
public sealed class CreditCardsInterop : ICreditCardsInterop
{
    private readonly IJSRuntime _jSRuntime;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncInitializer _scriptInitializer;

    private const string _module = "Soenneker.Blazor.CreditCards/js/creditcardsinterop.js";
    private const string _moduleName = "CreditCardsInterop";

    private readonly CancellationScope _cancellationScope = new();

    public CreditCardsInterop(IJSRuntime jSRuntime, IResourceLoader resourceLoader)
    {
        _jSRuntime = jSRuntime;
        _resourceLoader = resourceLoader;
        _scriptInitializer = new AsyncInitializer(InitializeScript);
    }

    private async ValueTask InitializeScript(CancellationToken token)
    {
        await _resourceLoader.LoadStyle(
            "_content/Soenneker.Blazor.CreditCards/css/creditcards.css",
            cancellationToken: token);

        await _resourceLoader.ImportModuleAndWaitUntilAvailable(
            _module,
            _moduleName,
            100,
            token);
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _scriptInitializer.Init(linked);
    }

    public async ValueTask Create(
        ElementReference container,
        ElementReference card,
        string id,
        CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            await _scriptInitializer.Init(linked);
            await _jSRuntime.InvokeVoidAsync("CreditCardsInterop.create", linked, container, card, id);
        }
    }

    public async ValueTask UpdateCardStyle(
        ElementReference card,
        CardStyle style,
        CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            await _scriptInitializer.Init(linked);
            await _jSRuntime.InvokeVoidAsync("CreditCardsInterop.updateCardStyle", linked, card, style);
        }
    }

    public async ValueTask Destroy(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _jSRuntime.InvokeVoidAsync("CreditCardsInterop.dispose", linked, id);
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_module);
        await _scriptInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
