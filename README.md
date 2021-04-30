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

Now you should have hot browser reload courtesy of _Westwind.AspnetCore.LiveReload_ for changes both in MVC project and Tailwind or JavaScript files.

Reference links:

* https://hotwire.dev/
* https://github.com/RickStrahl/Westwind.AspnetCore.LiveReload
* https://tailwindcss.com/

Good luck!