using XNode;

namespace ComfyuGUIEditor
{
    
    public class ComfyuGUIGraph: NodeGraph
    {
        public string graphName{get=>isDirty?relName+" *":relName;set=>relName = value;}
        public string relName { get; private set; }

        public bool isDirty { get; set; }
    }
}