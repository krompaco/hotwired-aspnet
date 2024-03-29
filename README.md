# krompaco/hotwired-aspnet

A Razor Pages sample site with CLI setup for Tailwind CSS and Webpack for Hotwire with Turbo and Stimulus.

## [Demo site on Azure](https://hotwired.azurewebsites.net/)

The demo web site is built using an Action in this repo.

It's the basic GitHub Action to Azure App Service example plus these steps first for building the frontend files.

```
- name: Setup npm
  uses: actions/setup-node@v2
  with:
    node-version: 18.12.0
- run: npm ci
- run: npm run prodbuild
```

## Getting started

Be in repository root and do:

```
npm ci
npm run dev
```

Or if you don't need any JS watched and built while working this will work faster:

```
npm run prodbuild
npm run dev:css
```

To build without watcher:

```
npm run prodbuild
```

I recommend doing `npm run dev:css` and then going to `src/WebApp/` and starting dotnet project by using:

```
dotnet watch run
```

Material used:

* https://hotwire.dev/
* https://tailwindcss.com/
* https://github.com/excid3/tailwindcss-stimulus-components
* https://django-turbo-response.readthedocs.io/en/latest/#channels
* https://docs.microsoft.com/en-us/aspnet/core/fundamentals/websockets?view=aspnetcore-5.0
* https://radu-matei.com/blog/aspnet-core-websockets-middleware/
* https://github.com/radu-matei/websocket-manager/
* https://raw.githubusercontent.com/jakakonda/View2String/master/View2String/Services/ViewRendererService2.cs

This article is where the SignalR setup comes from:
https://medium.com/@kpax/hotwired-asp-net-core-web-application-part-4-5ff84da54452

About Turbo Streams over Web Socket: I recommend using the SignalR way from `main` but in the branch `archived/barebone-websocket` there's a barebone Web Socket Proof-of-Concept as well.

An alternative dotnet project:
https://www.nuget.org/packages/HotwireTurbo/

## Roadmap

* Maybe make NuGet package of `Krompaco.AspNetCore.Hotwired`.
* XUnit tests for `Krompaco.AspNetCore.Hotwired`.
* Make default styling to look like an Optimizely CMS admin page.
