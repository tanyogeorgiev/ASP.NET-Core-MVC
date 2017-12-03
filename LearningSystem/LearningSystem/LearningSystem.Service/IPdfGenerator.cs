namespace LearningSystem.Service
{
    public interface IPdfGenerator
    {
        byte[] GeneratePdfFromThml(string html);
    }
}
