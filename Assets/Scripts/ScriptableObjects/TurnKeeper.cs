using UnityEngine;

[CreateAssetMenu]
public class TurnKeeper : ScriptableObject
{
	public TurnPhase CurrentTurnPhase
	{
		get;
		set;
    }

#if false

[x] Turns
[x] play cards phase
[x] consider turn order, or leave for another GitHub isse - don't need for MVP
[x] opponent performs the same phases

[ ] TODO: can end turn by clicking a bell in the center-left of the table
[ ] combat phase: play now proceeds into the combat phase
	[ ] monsters on the field can now attack the opponent directly (since the opponent can't yet place monsters)
	[ ] attack points will create tokens that accrue on the opponent's side of the scale

#endif
}
