using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.CreditCards.Abstract;
using Soenneker.Blazor.CreditCards.Dtos;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Blazor.CreditCards;

///<inheritdoc cref="ICreditCardsInterop"/>
public sealed class CreditCardsInterop : ICreditCardsInterop
{
    private readonly IJSRuntime _jSRuntime;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncSingleton _scriptInitializer;

    private const string _module = "Soenneker.Blazor.CreditCards/js/creditcardsinterop.js";
    private const string _moduleName = "CreditCardsInterop";

    public CreditCardsInterop(IJSRuntime jSRuntime, IResourceLoader resourceLoader)
    {
        _jSRuntime = jSRuntime;
        _resourceLoader = resourceLoader;

        _scriptInitializer = new AsyncSingleton(async (token, arg) =>
        {
            await _resourceLoader.LoadStyle("_content/Soenneker.Blazor.CreditCards/css/creditcards.css", cancellationToken: token);
            await _resourceLoader.ImportModuleAndWaitUntilAvailable(_module, _moduleName, 100, token);
            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return _scriptInitializer.Init(cancellationToken);
    }

    public async ValueTask Create(ElementReference container, ElementReference card, string id, CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Init(cancellationToken);

        await _jSRuntime.InvokeVoidAsync($"{_moduleName}.create", cancellationToken, container, card, id);
    }

    public async ValueTask UpdateCardStyle(ElementReference card, CardStyle style, CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Init(cancellationToken);

        await _jSRuntime.InvokeVoidAsync($"{_moduleName}.updateCardStyle", cancellationToken, card, style);
    }

    public ValueTask Destroy(string id, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeVoidAsync($"{_moduleName}.dispose", cancellationToken, id);
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_module);
        await _scriptInitializer.DisposeAsync();
    }
}
