## Workflow

0. [ ] Update readme with pull request link so that the diff enables opening a pull request.
1. [ ] Find sound effects from freesound.org.
2. [ ] Check the sound's license to ensure that I can use it.
3. [ ] Import sound into `Sounds/` folder.
4. [ ] If desired, combine sounds into one using Audacity.
5. [ ] Update `Sounds/Attribution.md` doc.
6. [ ] Expose new reference in the `AllTheSounds` ScriptableObject, and hook up the `AudioClip`. Alternatively, if you want to play the sound as its own AudioSource, then create the AudioSource and hook up the `AudioClip`. This decision is made based on how frequently the sound is played, though eventually we may just refactor to use a more robust third-party SoundManager.
7. [ ] Add test methods to `SoundManager` (or a separate script) that play the sound. If the audio clip is split into many sections, identify sections within the audio clip to play with the `AudioClipSection` class, then use `AudioSourceHelpers.PlaySoundInterval`.
8. [ ] Expose game event for when you want the sound to be played. For example, the `PlayerAnimationEvents` script exposes player animation events to C#. You may need to also impose certain conditions for playing the sound.
9. [ ] Make `SoundManager` (or your separate script) listen to the new game event, and call the corresponding play-sound method when the event occurs.

## Future alternative SFX production methods

* bfxr.net
* Purchase a reputable (not stolen, like Sidearm Studios') sound effect pack on the Asset Store
* Research other methods of procuring or producing sounds on Reddit
