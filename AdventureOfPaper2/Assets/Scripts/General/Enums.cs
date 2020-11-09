#region Player
public enum PlayerMoveState
{
    idle,
    walk,
    duck,
    attack,
    jump,
    pickingUp,
    carry,
    loweringObj,
    takingDamage,
}

public enum PlayerLookDir
{
    right,
    left
}

#endregion

public enum WorldMapPlayerLookDir
{
    down,
    left,
    up,
    right
}
public enum GameState
{
    running,
    pause
}

public enum StartPoint
{
    Apoint,
    Bpoint,
    Cpoint,
    Dpoint
}

public enum DoorState{
    Open,
    Close,
    Locked
}

public enum PickableObjState
{
    none,
    lifted,
    lowered,
}

#region Enemy

public enum EnemyAIState
{
    Spawning,
    Idling,
    Roaming,
    ChasingTarget,
    MovingForward,
    Jumping,
    Attacking,
    TakingDamage,
    Dying,
}

public enum  EnemySpawnerType
{
    Once,
    Wave,
    Boss,
    Infinite,
}

public enum EnemyAttackType
{
    Box,
    Circle,
}

public enum EnemyType
{
    standing,
    wandering,
    flying,
}

#endregion