# krompaco/hotwired-aspnet

A Razor Pages sample site with CLI setup for Tailwind CSS and Webpack for Hotwire with Turbo and Stimulus.

## Getting started

Be in repository root and do:

```
npm ci
npm run dev
```

Or if you don't need any JS built this will work faster:

```
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
* https://django-turbo-response.readthedocs.io/en/latest/#channels
* https://docs.microsoft.com/en-us/aspnet/core/fundamentals/websockets?view=aspnetcore-5.0
* https://radu-matei.com/blog/aspnet-core-websockets-middleware/
* https://github.com/radu-matei/websocket-manager/

Good luck!

## Roadmap

* Turbo examples
* Form examples
* Stream in POST response examples