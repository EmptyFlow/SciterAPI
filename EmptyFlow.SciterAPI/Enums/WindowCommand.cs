namespace EmptyFlow.SciterAPI {
    public enum WindowCommand : uint { // Original name SCITER_WINDOW_CMD
        SCITER_WINDOW_SET_STATE = 1, // p1 - SCITER_WINDOW_STATE, p2 - N/A
        SCITER_WINDOW_GET_STATE = 2, // p1 - N/A , p2 - N/A, returns SCITER_WINDOW_STATE
        SCITER_WINDOW_ACTIVATE = 3, // p1 - BOOL, true - bring_to_front , p2 - N/A
        SCITER_WINDOW_SET_PLACEMENT = 4, // p1 - const POINT*, position, p2 const SIZE* - dimension, in ppx, either one can be null
        SCITER_WINDOW_GET_PLACEMENT = 5, // p1 - POINT*, position, p2 SIZE* - dimension, in ppx, either one can be null

        SCITER_WINDOW_GET_VULKAN_ENVIRONMENT = 20, // p1 - &SciterVulkanEnvironment, p2 - sizeof(SciterVulkanEnvironment)
        SCITER_WINDOW_GET_VULKAN_CONTEXT = 21, // p1 - &SciterVulkanContext, p2 - sizeof(SciterVulkanContext)
        SCITER_WINDOW_SET_VULKAN_BRIDGE = 22, // p1 - SciterWindowVulkanBridge*, p2 - N/A
    }

}
