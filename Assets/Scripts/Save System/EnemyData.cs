namespace Save_System
{
    [System.Serializable]
    public class EnemyData
    {
        public int health;
        public float[] position;

        public EnemyData(EnemyController enemy)
        {
            health = enemy.CurrentHealth;
            
            position = new float[2];
            position[0] = enemy.transform.position.x;
            position[1] = enemy.transform.position.y;
        }
    }
}
