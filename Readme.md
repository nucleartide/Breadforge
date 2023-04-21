# 🥖 Breadforge

> *(trailer / GIF goes here soon...)*

A factory builder game about building a bread factory. Made in Unity 2021.

Simultaneously, this open source project demonstrates the complete skillset needed to ship a game: from idea to Steam launch. 🚀

<br />

## Motivation

Making games is hard.

Because when it comes time to make your idea come to life, you run into all sorts of roadblocks:

1. **Scope**. You're unsure how to scope your idea down into something manageable.
2. **Work breakdown**. You're unsure how to break your idea down into bite-sized development tasks.
3. **Progress**. Your codebase becomes tangled as you add features, so you struggle to make progress.

The result? You never ship your game, and all that time you spent was for naught.

But what if you knew exactly how to scope your idea down, while keeping the spirit of your original inspiration?

What if you knew exactly how to break your idea down into bite-sized tasks, so that you can start tackling them today?

And what if adding features to your game was a **joy**, not a chore?

You'd be eager to work on your game every day. And you’d ship your game with confidence and momentum.

But... how?

---

You need a role model.

A codebase that **demonstrates** and **speaks to** every skill of a professional game programmer — skills such as code quality, playtesting, production pipelines, and scope management.

And in doing so, the codebase would showcase every aspect of the game production process, so that you can apply its learnings to your own work.

That's what this project Breadforge provides.

Curious? Read below to learn more 👇

<br />

## Playful Production Process: a story told through pull requests

In the book *A Playful Production Process*, the author Richard Lemarchand breaks the process of game development into four stages: ideation, pre-production, production, and post-production.

Much like the chapters of a novel, I tell the story of Breadforge's development through its cumulative pull requests. For some of the pull requests, I also narrate their development in the form of a "painlog".

Devlogs are about *me*; painlogs are about *you*. Instead of describing what I've done, a painlog describes the general *pain point* behind a batch of pull requests, and what you can take away from my struggles.

You can view those chapters below:

<br />

## I. Pre-production

*Note that an ideation section is skipped to focus on the latter 3 stages of development.*

<br />

### Chapter 1. The 3 Cs Toolkit in Unity (2023)

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

### Chapter 2. How to implement a procedural generation system

> On generating a world using stacks of Perlin noise waves

| Feature | Pull Request |
| --- | --- |
| Populate game world with procedurally-generated resources | https://github.com/nucleartide/Baguettorio/pull/30 |
| Add actual resource assets and change to ortho cam | https://github.com/nucleartide/Baguettorio/pull/32 |

<br />

### Chapter 3. How to implement any gameplay system

> And why you should implement visual feedback first

| Feature | Pull Request |
| --- | --- |
| Highlight collectible resources | https://github.com/nucleartide/Breadforge/pull/37 |
| Add ability to collect resources | https://github.com/nucleartide/Breadforge/pull/38 |

<br />

### Chapter 4. How to add sound to your Unity game

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

### Chapter 5. How to integrate QA into your game dev process

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

### Chapter 6. How to create a look and feel for your Unity game

> And the importance of finding your game's visual "hook"

| Feature | Pull Request |
| --- | --- |
| Make everything unlit + copy assets over from Polyperfect asset pack | https://github.com/nucleartide/Breadforge/pull/58 |
| Add `ThirdPartyAssets/` as git submodule | https://github.com/nucleartide/Breadforge/pull/60 |
| Add `Stylized Water 2` to dependencies | https://github.com/nucleartide/Breadforge/pull/61 |
| Add `Stylized Water 2` test scene | https://github.com/nucleartide/Breadforge/pull/62 |
| *This is currently being worked on. Check back soon for updates!* | |

<br />

## II. Production

...

<br />

## III. Post-production

...

<br />

### Chapter X. How to publish a game on Steam

...

<br />

## Follow along

I hope that Breadforge offers something for everyone:

* **For game players**:
	* Once done, Breadforge will be a fun factory builder in the vein of Factorio.
* **For hiring managers**:
	* This project demonstrates the skillset of a professional game programmer, and serves as a good reference for required skills.
* **For new game programmers**:
	* This project teaches you how one might tackle each stage of the game dev process.
* **For fellow game programmers**:
	* This project inspires you through documentation and hard work. 🙂

Although I occasionally [give presentations](https://www.meetup.com/unity3d/events/291639476/), [write LinkedIn posts](https://www.linkedin.com/pulse/why-you-should-care-sops-game-development-devlog-jason-tu/), or [send an email newsletter](https://jasont.co/blog), I'm not the most consistent.

So the best way to follow along is to star and watch this GitHub repo.

<br />

---

> Jason Tu · [jasont.co](https://jasont.co/)
