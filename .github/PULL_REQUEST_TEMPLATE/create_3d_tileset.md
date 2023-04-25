## Workflow

1. Create a Rule Tile (Project → Create → 2D → Tiles → Rule Tile).
2. Create a Tile Palette (Window → 2D → Tile Palette).
3. Drag new Rule Tile onto palette.
4. On the new Tilemap (created by step 2 above), change the swizzle to XZY.
5. Select your rule tile in your palette, and paint on the new Tilemap.
6. Use the new Tilemap to define tile rules in your Rule Tile.
    1. Remember: red arrow means no hit, green arrow means hit.
    2. Add a default sprite to make the rule tile visible in your Tile Palette.
7. Once you feel you’ve covered enough edge cases, you’re done!
    1. There might be more edge cases, but it’s best to discover them when integrating your game end-to-end, so as to avoid unneeded work.
