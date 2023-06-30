namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeMovementSpeed>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakePart>(true, true, true, false, false, false, false, false, false);

        }

        static partial void Init(State state, ref ME.ECS.World.NoState noState) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeMovementSpeed>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakePart>(true, true, true, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(state, ref noState);


            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Snake.Components.SnakeInitializer>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Snake.Components.SnakeMovementSpeed>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Snake.Components.SnakePart>(ref state.allocator, true);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateDataUnmanaged<ProjectCore.Features.Snake.Components.SnakeInitializer>(false);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Snake.Components.SnakeMovementSpeed>(false);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Snake.Components.SnakePart>(true);

        }

    }

}
