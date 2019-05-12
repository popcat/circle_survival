namespace CircleSurvival
{
    public interface ISaveManager
    {
        void Save(SaveData data);
        SaveData Load();
    }
}
