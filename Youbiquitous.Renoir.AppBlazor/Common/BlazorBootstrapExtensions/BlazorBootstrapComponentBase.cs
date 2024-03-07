///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
//
// Original code from Blazor Bootstrap
//

using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Youbiquitous.Renoir.AppBlazor.Common.BlazorBootstrapExtensions;

public abstract class BlazorBootstrapComponentBase : ComponentBase, IDisposable, IAsyncDisposable
{
    private string @class;
    private HashSet<string> classList = new();
    private string classNames;
    private bool isClassDirty = true;
    private bool isStyleDirty = true;
    private string style;
    private HashSet<string> styleList = new();
    private string styleNames;
    private PriorityQueue<Func<Task>, RenderPriority> renderQueue;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        Rendered = true;
        PriorityQueue<Func<Task>, RenderPriority> renderQueue = this.renderQueue;
        if ((renderQueue != null ? (renderQueue.Count > 0 ? 1 : 0) : 0) != 0)
        {
            while (this.renderQueue.Count > 0)
                await this.renderQueue.Dequeue()();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    protected override void OnInitialized()
    {
        if (ShouldAutoGenerateId && ElementId == null)
            ElementId = IdGenerator.GetNextId();
        base.OnInitialized();
    }

    internal void AddClass(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;
        classList.Add(value);
    }

    internal void AddClass(string value, bool when)
    {
        if (!when || string.IsNullOrWhiteSpace(value))
            return;
        classList.Add(value);
    }

    internal void AddClass(IEnumerable<string> values)
    {
        if (values == null || !values.Any())
            return;
        classList.UnionWith(values);
    }

    internal void AddStyle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;
        styleList.Add(value);
    }

    internal void AddStyle(string value, bool condition)
    {
        if (!condition || string.IsNullOrWhiteSpace(value))
            return;
        styleList.Add(value);
    }

    internal void AddStyle(IEnumerable<string> values)
    {
        if (values == null || !values.Any())
            return;
        styleList.UnionWith(values);
    }

    public void Dirty() => isClassDirty = true;

    public void Dispose() => Dispose(true);

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        Dispose(false);
    }

    protected internal virtual void DirtyClasses() => isClassDirty = true;

    protected virtual void BuildClasses()
    {
        if (Class == null)
            return;
        AddClass(Class);
    }

    protected virtual void BuildStyles()
    {
        if (Style == null)
            return;
        AddStyle(Style);
    }

    protected virtual void DirtyStyles() => isStyleDirty = true;

    protected virtual void Dispose(bool disposing)
    {
        if (Disposed)
            return;
        if (disposing)
        {
            classList = null;
            styleList = null;
        }

        if (disposing && renderQueue != null)
        {
            renderQueue.Clear();
            renderQueue = null;
        }

        Disposed = true;
    }

    protected virtual ValueTask DisposeAsync(bool disposing)
    {
        try
        {
            if (!AsyncDisposed)
            {
                if (disposing)
                {
                    classList = null;
                    styleList = null;
                }

                AsyncDisposed = true;
            }

            return new ValueTask();
        }
        catch (Exception ex)
        {
            return new ValueTask(Task.FromException(ex));
        }
    }

    protected void QueueAfterRenderAction(Func<Task> action, RenderPriority renderPriority)
    {
        if (renderQueue == null)
            renderQueue = new PriorityQueue<Func<Task>, RenderPriority>();
        renderQueue.Enqueue(action, renderPriority);
    }

    protected bool AsyncDisposed { get; private set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; }

    [Parameter]
    public string Class
    {
        get => @class;
        set
        {
            @class = value;
            DirtyClasses();
        }
    }

    public string ClassNames
    {
        get
        {
            if (isClassDirty)
            {
                classList = new HashSet<string>();
                BuildClasses();
                classNames = classList.Any()
                    ? string.Join(" ", classList)
                    : null;
                isClassDirty = false;
            }

            return classNames;
        }
    }

    protected bool Disposed { get; private set; }

    [Parameter] public string ElementId { get; set; }

    public ElementReference ElementRef { get; set; }

    [Inject] protected IIdGenerator IdGenerator { get; set; }

    [Inject] protected IJSRuntime JS { get; set; }

    protected bool Rendered { get; private set; }

    protected virtual bool ShouldAutoGenerateId => false;

    [Parameter]
    public string Style
    {
        get => style;
        set
        {
            style = value;
            DirtyStyles();
        }
    }

    public string StyleNames
    {
        get
        {
            if (isStyleDirty)
            {
                styleList = new HashSet<string>();
                BuildStyles();
                styleNames = styleList.Any()
                    ? string.Join(";", styleList)
                    : null;
                isStyleDirty = false;
            }

            return styleNames;
        }
    }
}