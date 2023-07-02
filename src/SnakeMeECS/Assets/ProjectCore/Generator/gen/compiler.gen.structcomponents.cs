namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<ProjectCore.Components.Timer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Map.Components.CellInMatrix>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Map.Components.MapProperties>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeBody>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeHead>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeMoveDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeMovementSpeed>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakePartSpawn>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Food.Components.FoodChangePositionTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Food.Components.FoodInitializer>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Map.Components.CellInitializer>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakePartDestroy>(true, true, true, false, false, false, false, false, false);

        }

        static partial void Init(State state, ref ME.ECS.World.NoState noState) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<ProjectCore.Components.Timer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Map.Components.CellInMatrix>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Map.Components.MapProperties>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeBody>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeHead>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeMoveDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakeMovementSpeed>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakePartSpawn>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Food.Components.FoodChangePositionTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Food.Components.FoodInitializer>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Map.Components.CellInitializer>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<ProjectCore.Features.Snake.Components.SnakePartDestroy>(true, true, true, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(state, ref noState);


            state.structComponents.ValidateUnmanaged<ProjectCore.Components.Timer>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Map.Components.CellInMatrix>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Map.Components.MapProperties>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Snake.Components.SnakeBody>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Snake.Components.SnakeHead>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Snake.Components.SnakeInitializer>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Snake.Components.SnakeMoveDirection>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Snake.Components.SnakeMovementSpeed>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Snake.Components.SnakePartSpawn>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Food.Components.FoodChangePositionTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Food.Components.FoodInitializer>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Map.Components.CellInitializer>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<ProjectCore.Features.Snake.Components.SnakePartDestroy>(ref state.allocator, true);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateDataUnmanaged<ProjectCore.Components.Timer>(false);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Map.Components.CellInMatrix>(false);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Map.Components.MapProperties>(false);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Snake.Components.SnakeBody>(false);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Snake.Components.SnakeHead>(false);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Snake.Components.SnakeInitializer>(false);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Snake.Components.SnakeMoveDirection>(false);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Snake.Components.SnakeMovementSpeed>(false);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Snake.Components.SnakePartSpawn>(false);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Food.Components.FoodChangePositionTag>(true);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Food.Components.FoodInitializer>(true);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Map.Components.CellInitializer>(true);
            entity.ValidateDataUnmanaged<ProjectCore.Features.Snake.Components.SnakePartDestroy>(true);

        }

    }

}
