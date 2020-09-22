
/// <summary>
/// This interface will be used on any playable character to ensure they all have the same shared functionality
/// </summary>
namespace SuperBrosBros.Controllers
{
    public interface IPlayable
    {
        float MoveSpeed{ get; set; }
        float JumpForce{ get; set; }
        
        void Run();
        void Jump();
        void OnDeath();

    }
}