using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3d;
using ObjLoader.Loader.Loaders;
using OpenTK;

namespace RayTracer
{
    class ModelMateriaLoader : IMaterialStreamProvider
    {
        private string _pathToMaterialFolder;

        public ModelMateriaLoader(string pathToMaterialFolder)
        {
            _pathToMaterialFolder = pathToMaterialFolder;
        }

        public Stream Open(string materialFilePath)
        {
            return File.Open(_pathToMaterialFolder + "/" + materialFilePath, FileMode.Open, FileAccess.Read);
        }
    }

    class SceneObject
    {
        private LoadResult _model;
        private BoundingBox _boundingBox;

        private SceneObject(LoadResult model)
        {
            Model = model;
            var tmpTriangles = new List<Triangle>();
            foreach (var group in Model.Groups)
            {
                foreach (var face in group.Faces)
                {
                    if (face.Count != 3)
                        throw new Exception("Model is not triangulated");

                    var vert1 = Model.Vertices[face[0].VertexIndex-1];
                    var vert2 = Model.Vertices[face[1].VertexIndex-1];
                    var vert3 = Model.Vertices[face[2].VertexIndex-1];

                    var norm1 = Model.Normals[face[0].NormalIndex - 1];
                    var norm2 = Model.Normals[face[1].NormalIndex - 1];
                    var norm3 = Model.Normals[face[2].NormalIndex - 1];

                    var v1 = new Vector3(vert1.X, vert1.Y, vert1.Z);
                    var v2 = new Vector3(vert2.X, vert2.Y, vert2.Z);
                    var v3 = new Vector3(vert3.X, vert3.Y, vert3.Z);

                    var n1 = new Vector3(norm1.X, norm1.Y, norm1.Z);
                    var n2 = new Vector3(norm2.X, norm2.Y, norm2.Z);
                    var n3 = new Vector3(norm3.X, norm3.Y, norm3.Z);

                    var n = Vector3.Add(Vector3.Add(n1, n2), n3);
                    n.Normalize();

                    tmpTriangles.Add(new Triangle(v1, v2, v3, n));
                }
            }

            Triangles = tmpTriangles;
        }

        public static SceneObject LoadFromFile(string path)
        {
            //var result = FileFormatObj.Load(path, false);

            var objLoaderFactory = new ObjLoaderFactory();
            IObjLoader objLoader = null;

            var lastIndex = path.LastIndexOf("/", StringComparison.Ordinal);
            if (lastIndex != -1)
            {
                var msp = new ModelMateriaLoader(path.Substring(0, lastIndex));
                objLoader = objLoaderFactory.Create(msp);
            }
            else
            {
                objLoader = objLoaderFactory.Create();
            }

            var fileStream = new FileStream(path + ".obj", FileMode.Open, FileAccess.Read);

            return new SceneObject(objLoader.Load(fileStream));
        }

        public LoadResult Model
        {
            get { return _model; }
            set
            {
                _model = value;
                _boundingBox = BoundingBox.FromVertices(_model.Vertices);
            }
        }

        public BoundingBox BoundingBox
        {
            get { return _boundingBox; }
        }

        public IReadOnlyCollection<Triangle> Triangles { get; private set; } 
    }
}
