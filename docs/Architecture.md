# Architecture

The LSS Office Suite is organized around a layered command architecture.

```text
Ribbon -> Command -> Service -> Word API
```

The Ribbon contains only UI callbacks. All behavior is delegated to commands and services resolved from dependency injection.

## Layers

- `LSS.WordAddIn`: VSTO host, ribbon, startup pipeline.
- `LSS.Core`: contracts and command abstractions.
- `LSS.Infrastructure`: logging, settings, file system services.
- `LSS.Word`: Word COM wrapper services.
- `LSS.UI`: WinForms dialogs and task panes.
- Feature modules: Legislative, AI, GraphQL, PDF, etc.
