using ME.ECS;

namespace ProjectCore.Features.Snake.Views {
    
    using ME.ECS.Views.Providers;
    
    public class SnakePartView : MonoBehaviourView {
        
        public override bool applyStateJob => true;

        public override void OnInitialize() 
        {
            
        }
        
        public override void OnDeInitialize() 
        {
            
        }
        
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) 
        {
            
        }
        
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
        }
        
    }
    
}