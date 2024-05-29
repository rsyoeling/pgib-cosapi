class HandleSelectionExtension extends Autodesk.Viewing.Extension {
    constructor(viewer, options) {
        super(viewer, options);
        this._group = null;
        this._button = null;
    }

    load() {
        console.log('Dar avance cargado correctamente.');
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
        console.log('Dar avance no disponible');
        return true;
    }
    

    onToolbarCreated() {
        // Create a new toolbar group if it doesn't exist
        this._group = this.viewer.toolbar.getControl('allMyAwesomeExtensionsToolbar');
        if (!this._group) {
            this._group = new Autodesk.Viewing.UI.ControlGroup('allMyAwesomeExtensionsToolbar');
            this.viewer.toolbar.addControl(this._group);
        }
        
          
        
        // Add a new button to the toolbar group
        this._button = new Autodesk.Viewing.UI.Button('handleSelectionExtensionButton');
        this._button.onClick = (ev) => {
            console.log('restablecer valores');
            document.getElementById('txtAvance').value = '';
            document.getElementById('txtEstadoAvance').value = '';
            document.getElementById('f_ejecucion').valueAsDate = new Date();
            //document.getElementById('f_ejecucion').value = '';
            document.getElementById('f_planificada').value = '';
            //val igual a ''
            
//this.viewer.clearSelection();
            this.viewer.clearThemingColors();
            
            var elementos = []; 
            //var elementos1 = ['006d8900-7d1a-47a2-b276-cb1144bf102f-0028af70','006d8900-7d1a-47a2-b276-cb1144bf102f-0028b045','5804ae82-1628-405b-871e-f1e7eaba771d-00103d85']; 
            
            // Execute an action here
            
            // Get current selection
            let mi_visor = this.viewer;
            const selection = this.viewer.getSelection();
            console.log(selection);
            // Anything selected?
            if (selection.length > 0) {
                var busqueda ="";
                var isolated = [];
                // Iterate through the list of selected dbIds
                var uri = "http://localhost:3500/updateAvanceExtIdacero";
                
                selection.forEach((dbId) => {
                    console.log(selection.length);
                    // Get properties of each dbId
                    isolated.push(dbId);
                    this.viewer.getProperties(dbId, (props) => {
                        // Output properties to console
                        //console.log(dbId);
                        //console.log('hola')
                        //viewer.setThemingColor(dbId,new THREE.Color(0xFF0000), 0.1);
                        //elementos.push(props.properties["COSAPI-ID-ELEMENTO"]);
                        elementos.push(dbId);
                    })
                });    
                        //console.log(elementos);
                        $('#modalCRUD1').modal('show');
                       
                        /* for (let i = 0; i < elementos.length; i++) {                    
                            if (i-1 == elementos.length) {
                            busqueda = ''+busqueda + elementos[i];
                            } 
                            else { busqueda = ''+busqueda + elementos[i] + '|' }
                            }
                        console.log(elementos); */
                        $('#formArticulos1').submit(function (e) {
                            e.preventDefault();
                            let avance = $.trim($('#txtAvance').val());
                            let e_avance = $('#txtEstadoAvance').val();
                            let f_ejecucion = document.getElementById('f_ejecucion').value;
                            let f_planificada = document.getElementById('f_planificada').value;
                            
                            if (f_ejecucion == null){
                                f_ejecucion = '0000-00-00'
                            }
                            if (f_planificada == null){
                                f_planificada = '0000-00-00'
                            }
                            console.log(avance + ' ' + e_avance + ' ' + f_ejecucion + ' ' + f_planificada + ' ' + elementos);
                            //console.log(avance);
                            //console.log(fecha);
                            elementos.forEach(function(elemento, indice,array){
                                $.ajax({
                                  url: uri,
                                  //url: 'http://srvscopepru:3500/updateAvanceExtId',
                                  method: 'put',
                                  contentType: 'application/json',
                                  data: JSON.stringify({ avance: avance, e_avance: e_avance, id_elemento: elemento , f_ejecutada : f_ejecucion }),
                                  dataType: "json",
                                  //mostrar modal de carga
                                    beforeSend: function () {
                                        //new pNotify({ title: 'Cargando', text: 'Espere un momento por favor', type: 'info', styling: 'bootstrap3' });
                                    },
                                  success: function (data) {
                                    $('#modalCarga').modal('hide');
                                      //var resp = JSON.parse(data)
                                      //tablaArticulos.ajax.reload(null, false);
                                      console.log('Elementos actualizados');
                                        
                                  }
                              })
                              });

                            /* table.ajax.reload(); */
                            $('#modalCRUD1').modal('hide');
                            new PNotify({
                                title: 'Actualizando...',
                                text: 'Se actualizaron ' + elementos.length + ' elementos.',
                                addclass: 'bg-success border-success'
                            });
                            //new pNotify({ title: 'Actualizado', text: 'Se actualizaron' + elementos.length + 'elementos.', type: 'success', styling: 'bootstrap3' });  
                            //swal("Actualizado!", "Elementos actualizados", "success");
                            mi_visor.clearSelection();
                        
                        
                          /* this.viewer.isolate(selection.dbId); */
                        /* this.viewer.isolate([24945,
                            24947,
                            24981,
                            24983]); */
                        
                        /* table.search( busqueda.substring(0, busqueda.length - 1),true,false ).draw(); */
                    });
                    
                    
                /*     for (let i = 0; i < elementos.length; i++) {                    
                        if (i-1 == elementos.length) {
                        busqueda = ''+busqueda + elementos[i];
                        } 
                        else { busqueda = ''+busqueda + elementos[i] + '|' }
                        } 
                        */
                
                              
                //console.log(isolated);
                this.viewer.isolate(isolated)
            } else {
                // If nothing selected, restore
                this.viewer.isolate(0);
            }
        };
        this._button.setToolTip('Dar avance');
        this._button.addClass('handleSelectionExtensionIcon');
        this._group.addControl(this._button);
    }
}

Autodesk.Viewing.theExtensionManager.registerExtension('HandleSelectionExtension1', HandleSelectionExtension);
