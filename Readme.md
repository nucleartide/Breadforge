# 🥖 Breadforge

A factory builder game about building a bread factory. Made in Unity 2021.

<br />

## A story told through pull requests

Much like the chapters of a novel, I tell the story of Breadforge's development through its cumulative pull requests. For some of the pull requests, I also narrate their development in the form of a "painlog".

Devlogs are about *me*; painlogs are about *you*. Instead of describing what I've done, a painlog describes the general *pain point* behind a batch of pull requests, the technical approach, as well as benefits and tradeoffs.

You can view those chapters below:

<br />

## Chapter 1. The 3 Cs Toolkit in Unity (2023)

> Make your character come to life with Mecanim, Cinemachine, and Input System.

The 3 Cs are the basic building blocks of any game with a 3D character avatar.

In March 2023, I gave a presentation at the [Unity NYC meetup](https://www.meetup.com/unity3d/events/291639476/) about how to implement the 3 Cs in Unity.

You can read the blog post behind the presentation here: [jasont.co/character-creation-toolkit](https://jasont.co/character-creation-toolkit/)

| Feature | Pull Request |
| --- | --- |
| Implement basic character animation | https://github.com/nucleartide/Baguettorio/pull/25 |
| Implement Cinemachine third-person camera | https://github.com/nucleartide/Baguettorio/pull/26 |
| Refactor game input to use Unity's new Input System package | https://github.com/nucleartide/Baguettorio/pull/27 |
| Refactor to get rid of CharacterController | https://github.com/nucleartide/Baguettorio/pull/28 |
| Add Pause button as a reference point for C# events usage | https://github.com/nucleartide/Baguettorio/pull/29 |

<br />

## Chapter 2. How to implement a procedural generation system

> On generating a world using stacks of Perlin noise waves

| Feature | Pull Request |
| --- | --- |
| Populate game world with procedurally-generated resources | https://github.com/nucleartide/Baguettorio/pull/30 |
| Add actual resource assets and change to ortho cam | https://github.com/nucleartide/Baguettorio/pull/32 |

<br />

## Chapter 3. How to implement any gameplay system

> And why you should implement visual feedback first

| Feature | Pull Request |
| --- | --- |
| Highlight collectible resources | https://github.com/nucleartide/Breadforge/pull/37 |
| Add ability to collect resources | https://github.com/nucleartide/Breadforge/pull/38 |

<br />

## Chapter 4. How to add sound to your Unity game

> And why it's the most important kind of game juice

I wrote a [blog post on LinkedIn](https://www.linkedin.com/pulse/why-you-should-care-sops-game-development-devlog-jason-tu/) that summarizes the changes below.

| Feature | Pull Request |
| --- | --- |
| Add backing soundtrack | https://github.com/nucleartide/Breadforge/pull/39 |
| Add mine sound effect + sound effect process | https://github.com/nucleartide/Breadforge/pull/40 |
| Add footstep sounds for walking and running | https://github.com/nucleartide/Breadforge/pull/41 |
| Add footstep sounds for walking and running... on rocks | https://github.com/nucleartide/Breadforge/pull/42 |
| Add guardrail sounds | https://github.com/nucleartide/Breadforge/pull/43 |
| Add "null" (nothing to mine) mining sound | https://github.com/nucleartide/Breadforge/pull/44 |
| Add collect water sound | https://github.com/nucleartide/Breadforge/pull/45 |
| Add chop sounds for thin, medium, and thicc wood | https://github.com/nucleartide/Breadforge/pull/46 |

<br />

## Chapter 5. How to integrate QA into your game dev process

> And why you should always fix bugs first

| Feature | Pull Request |
| --- | --- |
| Bugfixes: make resource collection not continuous + add polish for sound feedback | https://github.com/nucleartide/Breadforge/pull/47 |
| Bugfix: shift ground by .5 so that all tiles appear on ground platform | https://github.com/nucleartide/Breadforge/pull/48 |
| Fix showstopper bugs for last release | https://github.com/nucleartide/Breadforge/pull/49 |
| Fix animation bugs for last release | https://github.com/nucleartide/Breadforge/pull/50 |
| Add ability to zoom in and out | https://github.com/nucleartide/Breadforge/pull/52 |
| Fix UI camera ortho size not updating; make sounds 3D via spatialBlend param | https://github.com/nucleartide/Breadforge/pull/53 |
| Add a ton of animation polish | https://github.com/nucleartide/Breadforge/pull/54 |

<br />

## Chapter 6. How to create a look and feel for your Unity game

> And the importance of finding your game's visual "hook"

| Feature | Pull Request |
| --- | --- |
| Make everything unlit + copy assets over from Polyperfect asset pack | https://github.com/nucleartide/Breadforge/pull/58 |
| Add `ThirdPartyAssets/` as git submodule | https://github.com/nucleartide/Breadforge/pull/60 |
| Add `Stylized Water 2` to dependencies | https://github.com/nucleartide/Breadforge/pull/61 |
| Add `Stylized Water 2` test scene | https://github.com/nucleartide/Breadforge/pull/62 |
| Replace water tiles with Stylized Water 2 | https://github.com/nucleartide/Breadforge/pull/63 |
| Add ground rule tile + tile palette + example scene | https://github.com/nucleartide/Breadforge/pull/64 |
| Integrate 3D tileset with stylized water into current game scene | https://github.com/nucleartide/Breadforge/pull/65 |
| Add rock textures + grass biome | https://github.com/nucleartide/Breadforge/pull/66 |
| Make island into a circular shape | https://github.com/nucleartide/Breadforge/pull/67 |
| Add camera rotation script for recording a TikTok | https://github.com/nucleartide/Breadforge/pull/68 |

<br />

## Chapter 7. Chasing the first playable

> And the importance of finding your game's mechanical hook

| Feature | Pull Request |
| --- | --- |
| Add inventory system | https://github.com/nucleartide/Breadforge/pull/69 |
