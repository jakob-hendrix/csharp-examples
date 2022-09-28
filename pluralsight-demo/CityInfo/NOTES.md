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

## Models

- DTO
  - not the same as entities
  - may contain calculated values


## HTTP Responses

- if a request for an unmapped routes - 400
- if a rqeuest to a valud endpoint, but no returned resource, default rresponse is 200 with null data. This is bad.
- common status codes
  - level 100 - info
  - level 200 - good request
    - 200 -ok
    - 201 - created
    - 204 - no content
  - level 300 - redirect
  - level 400 - client mistakes
    - 400 - bad request (unparsable json, eg)
    - 401 - unauth
    - 403 - forbidden
    - 404 - not found
    - 409 - conflict (edit conflict b/w 2 updates)
  - level 500 - server mistakes
    - 500 - internal server error - client should try again
- controller base has helper methods

## Sending Files

- you can totaly just send files out of an api. Cool.

## Content Negotiation

- selectt the best reporesentaion for a given response
  - formatters
    - media tpe passed through Accept header
      - application/json (default)
      - application/xml
    - ASP.NET Core output formatters (accept header) & input formatters (content-type header)
      - support buoilt into ObjectResult
## Common Mistakes

- returning 200 when something is wrong
- dont sent 500 when client makes a mistake
