# MML (Mempler's markup language)
MML is an easy way to create osu!Framework UI Elements without much code.
it is fully dynamic and can be easily expanded.

## Examples
```cs
class YourGame : Game
{
    [BackgroundDependencyLoader]
    private void load()
    {
        var xamlData = "<oml>" +
                            "<Box width=\"250px\" height=\"250px\" colour=\"#FF0000\" position=\"0,0\"/>" +
                        "</oml>"
        // Create a parser from our xamlData.
        var parser = new MmlParser(xamlData);
        // Pass our parser into our DisplayContainer so it'll construct on "Add"
        var display = new MmlDisplayContainer(parser);

        Child = display;
    }
}
```

```xml
<mml>
    <Box width="250px" height="250px" colour="#FF0000" position="0,0"/>
    <Box width="2.6041in" height="2.6041in" colour="rgba(0, 0, 255, .4)" position="100,100"/>
    <Box width="187.5pt" height="187.5pt" colour="rgba(0, 255, 255, .6)" position="200,200"/>
    <Box width="15.625pc" height="15.625pc" colour="rgba(255, 0, 255, .8)" position="300,300"/>
</mml>
```

```xml
<mml>
    <BufferedContainer blurSigma="4,-4" cacheDrawnFrameBuffer="true">
        <sprite texture="https://a.ppy.sh/10291354"
            width="250"
            height="250"
        />
    </BufferedContainer>
</mml>
```

```xml
<mml>
    <container width="250" height="250">
        <box width="250" height="250" colour="orange" />
        <sprite texture="https://a.ppy.sh/10291354" width="200" height="200" margin="25" />
    </container>
</mml>
```

more examples [here](./MML.VisualTests/)

## License
MML is licensed under MIT which basically means you can do what ever you want
as long as you credit me.
