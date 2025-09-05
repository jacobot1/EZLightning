# EZLightning
Ever wanted to spawn in-game lightning at will? This API makes it SUPER easy.
## Dev Setup
> Make sure you are using .NET Standard 2.1 for best compatibility.

Simply add the .dll as a dependency or assembly reference, and make sure you tell BepInEx about the dependency by including `[BepInDependency("com.jacobot5.EZLightning")]` over your main plugin class. If you release your mod to Thunderstore, make sure to follow the store's syntax for dependencies in your `manifest.json`.
## Usage
It's literally a one-liner.
``` cs
EZLightning.API.Strike(Vector3 destination)
```
If you want a custom starting point for your lightning bolt, use:
``` cs
EZLightning.API.Strike(Vector3 destination, Vector3 origin)
```
For more customization, see the comments.

Made with love by a Great Asset.
