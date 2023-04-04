## Pipeline

Copy and paste the list below:

1. [ ] Find a sound effect from freesound.org.
2. [ ] Check the sound's license to ensure that I can use it.
3. [ ] Import sound into `Sounds/` folder.
4. [ ] Update `Sounds/Attribution.md` doc.
5. [ ] Expose new reference in the `AllTheSounds` ScriptableObject, and hook up the `AudioClip`. Alternatively, if you want to play the sound as its own AudioSource, then create the AudioSource and hook up the `AudioClip`.
6. [ ] Add test methods to `SoundManager` (or a separate script) that play the sound. If the audio clip is split into many sections, identify sections within the audio clip to play with the `AudioClipSection` class, then use `AudioSourceHelpers.PlaySoundInterval`.
7. [ ] Expose game event for when you want the sound to be played. For example, the `PlayerAnimationEvents` script exposes player animation events to C#.
8. [ ] Make `SoundManager` (or your separate script) listen to the new game event, and call the corresponding play-sound method when the event occurs.
9. [ ] Update readme with pull request link.

## Future alternatives to consider

* bfxr.net
* Purchase a reputable (not stolen, like Sidearm Studios') sound effect pack on the Asset Store
* Research other methods of procuring or producing sounds on Reddit
