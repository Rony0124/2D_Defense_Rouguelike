using UI.Core;

namespace UI.InGame
{
    public class InGameViewModel : ViewModelBase
    {
       private InGameView inGameView => view as InGameView;
       private InGameModel inGameModel => model as InGameModel;

       public override void Awake()
       {
           base.Awake();
           
       }
    }
}
