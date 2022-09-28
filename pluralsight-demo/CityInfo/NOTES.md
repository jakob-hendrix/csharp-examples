# Notes

Course URL: https://app.pluralsight.com/library/courses/asp-dot-net-core-6-web-api-fundamentals/table-of-contents

## MVC

- model-view-controller
- typically in presentation layer
- parts
  - model - can contain logic (sometimes logic is always ina  biz layer)
    - viewmodels
  - view
  - controller
    - chooses the view to display to user, and provides it with model data
    - when requests are made, an action is triggered on the controller

## Routing

- maps request URI to action on a controller
  - app.UseRouting() - marks the pos in mw pipeline where routing decision is made
  - app.UseEndpoints() - marks pos in mw where sleected ep is executed
- attribute routing - route at controller and action level
  - route at controller level
    - ```[Route("api/[controller])]```
