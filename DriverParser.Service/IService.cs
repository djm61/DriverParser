using System.Threading.Tasks;

namespace DriverParser.Service
{
    public interface IService
    {
        string StatusMessage { get; set; }
        void ParseFile();
    }
}
