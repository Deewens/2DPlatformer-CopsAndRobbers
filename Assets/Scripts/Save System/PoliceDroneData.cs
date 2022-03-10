namespace Save_System
{
    [System.Serializable]
    public class PoliceDroneData
    {
        public int health;
        public float[] position;

        public PoliceDroneData(PoliceDroneController policeDrone)
        {
            
            health = policeDrone.CurrentHealth;
            
            position = new float[2];
            position[0] = policeDrone.transform.position.x;
            position[1] = policeDrone.transform.position.y;
        }
    }
}
