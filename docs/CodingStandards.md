# Coding Standards

- No business logic in Ribbon callbacks.
- Use constructor dependency injection.
- Keep Office COM types inside `LSS.Word` and `LSS.WordAddIn` whenever possible.
- Prefer services over static helpers.
- Log every command start, completion, and exception.
- Long-running work should use progress dialogs and cancellation where practical.
