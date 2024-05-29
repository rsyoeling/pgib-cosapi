class MyAwesomeExtension extends Autodesk.Viewing.Extension {
  constructor(viewer, options) {
      super(viewer, options);
      this._group = null;
      this._button = null;
  }
  
  load() {
      console.log('Avance Masivo cargado correctamente.');
      return true;
  }



  unload() {
      // Clean our UI elements if we added any
      if (this._group) {
          this._group.removeControl(this._button);
          if (this._group.getNumberOfControls() === 0) {
              this.viewer.toolbar.removeControl(this._group);
          }
      }
      console.log('Avance Masivo no disponible');
      return true;
  }




  onToolbarCreated() {

      var table = $('#tablaArticulos').DataTable();
      // Create a new toolbar group if it doesn't exist
      this._group = this.viewer.toolbar.getControl('allMyAwesomeExtensionsToolbar');
      if (!this._group) {
          this._group = new Autodesk.Viewing.UI.ControlGroup('allMyAwesomeExtensionsToolbar');
          this.viewer.toolbar.addControl(this._group);
      }
      var table = $('#tablaArticulos').DataTable();
      // Add a new button to the toolbar group
      this._button = new Autodesk.Viewing.UI.Button('myAwesomeExtensionButton');
      this._button.onClick = (ev) => {
        // Execute an action here
        // Get current selection
        if (sessionStorage.getItem("urn") == "dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdDJfY2hpbGVfYW50b2ZhZ2FzdGEvQ1IzMDA1MS1URS1CTS1BQy0wMDAzLnJ2dA==") {
            var uri = "http://localhost:3500/getAvanceEjecutadoXelementoAcero30051";
        } else {
            if (sessionStorage.getItem("urn") == "dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdDJfY2hpbGVfYW50b2ZhZ2FzdGEvQ1IzMDA1MS1URS1CTS1BLTAwMTEucnZ0") {
                var uri = "http://localhost:3500/getAvanceEjecutadoXelementoArqui30051";
            } else {
                if (sessionStorage.getItem("urn") == "dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdDJfY2hpbGVfYW50b2ZhZ2FzdGEvQ1IzMDA1MS1URS1CTS1TLTAwNjgucnZ0") {
                    var uri = "http://localhost:3500/getAvanceEjecutadoXelementoEst30051";
                }
                else {
                    if (sessionStorage.getItem("urn") == "dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdDJfY2hpbGVfYW50b2ZhZ2FzdGEvQ1IzMDA1MS1URS1CTS1NLTAwNjcucnZ0") {
                        var uri = "http://localhost:3500/getAvanceEjecutadoXelementoM30051";
                    }
                    else {
                        if (sessionStorage.getItem("urn") == "dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdDJfY2hpbGVfYW50b2ZhZ2FzdGEvQ1IzMDA1MS1URS1CTS1FLTAwNTUucnZ0") {
                            var uri = "http://localhost:3500/getAvanceEjecutadoXelementoE30051";
                        }
                        else {
                            if (sessionStorage.getItem("urn") == "dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdDJfY2hpbGVfYW50b2ZhZ2FzdGEvQ1IzMDA1MS1URS1CTS1GUC0wMDA0LnJ2dA==") {
                                var uri = "http://localhost:3500/getAvanceEjecutadoXelementoFP30051";
                            }
                else {
                    swal("Error", "Seleccione un modelo válido.", "error");
                }
            }
        }}}}
        
        

        const selection = this.viewer.getSelection();
        this.viewer.clearSelection();
        this.viewer.clearThemingColors();
        // Anything selected?
        var viewer_model = this.viewer;
        
        let id_ext = [];
        let isolates=[];
        
            let data = [];
            let azul = []; //this.viewer.setThemingColor(772875,new THREE.Vector4(-5, -6, 2, 2))
            var verde = []; //this.viewer.setThemingColor(772875,new THREE.Vector4(0, 5, 0, 2))
            var amarillo = []; //this.viewer.setThemingColor(772875,new THREE.Vector4(1, 2, 0, 2))
            var rojo = []; //viewer_model.setThemingColor(element.revit_objectid,new THREE.Vector4(1, 0, 0, 1));
            var red = new THREE.Vector4(1, 0, 0, 1);
            /* $.get("http://localhost/getAvanceEjecutadoXelemento", function (data, status) {
                                   
                                    
                                }
                                ); */
                                $.ajax({
                                    url: uri,
                                    type: "GET",
                                    
                                    async: true,
                                    cache: false,
                                    contentType: "application/json",
                                    success: function (response) {
                                    //convert json response to array of numbers
                                    
                                    /* console.log(response); */
                                    
                                    response.forEach(element => {
                                        isolates.push(parseInt(element.revit_objectid));
                                        //viewer_model.setThemingColor(element.revit_objectid,new THREE.Vector4(1, 0, 0, 1));
                                        if(element.avance < 0.25){
                                            azul.push(parseInt(element.revit_objectid));
                                        }else{
                                            if(element.avance < 0.5){
                                                verde.push(parseInt(element.revit_objectid));
                                            }else{
                                                if(element.avance < 0.75){
                                                    amarillo.push(parseInt(element.revit_objectid));
                                                }else{
                                                    rojo.push(parseInt(element.revit_objectid));
                                                }
                                            }
                                        }
                                        
                                    });
                                    azul.forEach(element => {
                                        viewer_model.setThemingColor(element,new THREE.Vector4(-5, -6, 2, 2));
                                    });
                                    verde.forEach(element => {
                                        viewer_model.setThemingColor(element,new THREE.Vector4(0, 5, 0, 2));
                                    });
                                    amarillo.forEach(element => {
                                        viewer_model.setThemingColor(element,new THREE.Vector4(1, 2, 0, 2));
                                    });
                                    rojo.forEach(element => {
                                        viewer_model.setThemingColor(element,new THREE.Vector4(1, 0, 0, 1));
                                    });
                                    viewer_model.isolate(isolates);
                                    
                                    },
                                    error: function (error) {
                                    console.log(error)
                                        
                                    }
                                    })                   
            
           

          
        
        
    };
      this._button.setToolTip('Visualizar Avance');
      this._button.addClass('myAwesomeExtensionIcon');
      this._group.addControl(this._button);
  }
}

Autodesk.Viewing.theExtensionManager.registerExtension('MyAwesomeExtension', MyAwesomeExtension);
