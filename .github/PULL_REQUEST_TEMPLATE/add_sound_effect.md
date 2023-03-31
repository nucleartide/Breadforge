## Pipeline

1. [ ] Find a sound effect from freesound.org. (I like this most currently, no need to download nor pay for huge asset pack.)
2. [ ] Check the sound's license to ensure that I can use it.
3. [ ] Import sound into `Sounds/` folder.
4. [ ] Update `Sounds/Attribution.md` doc. Give credit where it's due!
5. [ ] Expose new reference in the `AllTheSounds` ScriptableObject, and hook up the `AudioClip`.
6. [ ] Expose game event for when you want the sound to be played.
    * For example, the `PlayerAnimationEvents` script exposes player animation events to C#.
7. [ ] Make `SoundManager` listen to the new game event, and play the corresponding sound when the event occurs.

## Alternatives

* bfxr.net
* Purchase a reputable (not stolen, like Sidearm Studios') sound effect pack on the Asset Store
* Research other methods of procuring or producing sounds on Reddit
