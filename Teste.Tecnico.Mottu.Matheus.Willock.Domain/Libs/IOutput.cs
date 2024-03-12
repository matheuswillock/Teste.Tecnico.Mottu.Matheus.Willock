namespace Teste.Tecnico.Mottu.Matheus.Willock.Domain.Libs
{
    public interface IOutput
    {
        public void AddResult(object obj);
        public object GetResult();
        public void AddErrorMessage(string error);
        public void AddMessage(string error);
    }
}
