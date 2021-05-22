# hotwire-dotnet-mvc-ref
The regular 5.0 MVC sample but with Webpack setup for Tailwind CSS and Hotwire with Turbo and Stimulus.

## Getting started

Be in repository root and do:

```
npm install
npm run dev
```

To build without watcher:

```
npm run devbuild
```

To build with Tailwind CSS purge:

```
npm run prodbuild
```

I recommend doing `npm run dev` and then going to `src/WebApp/` and starting dotnet project doing watch there too:

```
dotnet watch run
```

References used:

* https://hotwire.dev/
* https://tailwindcss.com/
* https://django-turbo-response.readthedocs.io/en/latest/#channels
* https://docs.microsoft.com/en-us/aspnet/core/fundamentals/websockets?view=aspnetcore-5.0
* https://radu-matei.com/blog/aspnet-core-websockets-middleware/
* https://github.com/radu-matei/websocket-manager/

Good luck!