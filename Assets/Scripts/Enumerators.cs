namespace Enumerators
{
    public enum GamePhases
	{
		NONE,
		THE_START,
		GETTING_SERIOUS,
		THE_LAST_EFFORT,
		BRIEF,
		WIN_MAX,
		WIN_HIGH, 
		WIN_LOW,
		LOSS_HIGH,
		LOSS_LOW,
		LOSS_WORK_LOW,
		LOSS_WORK_HIGH,
		LOSS_STRESS_LOW,
		LOSS_STRESS_HIGH,
		LOSS_PEOPLE_LOW,
		LOSS_PEOPLE_HIGH,
		COUNT
	}
	public enum EncounterType { SINGLE, MULTIPLE, QUEST, Q_SINGLE, Q_MULTI }
    public enum Stats   {WORK, STRESS, PEOPLE, COUNT}
    public enum AudioClips   {TAP, ENCOUNTER_EXIT, ENCOUNTER_ENTER, BGM_LOOP, VICTORY, LOSS }
    public enum AnswerBlocks {LEFT, RIGHT}
    public enum EncounterBreaker {LEFT, RIGHT, NONE, END}
    
    public enum Options { SOUND, VIBRATION }

	public enum Constraint { LANDSCAPE, PORTRAIT }

	public enum AnchorType
	{
		BOTTOMLEFT,
		BOTTOMCENTER,
		BOTTOMRIGHT,
		MIDDLELEFT,
		MIDDLECENTER,
		MIDDLERIGHT,
		TOPLEFT,
		TOPCENTER,
		TOPRIGHT,
	};
}
