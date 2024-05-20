class SummaryIcon extends Autodesk.Viewing.Extension {
    constructor(viewer, options) {
        super(viewer, options);
        this._group = null;
        this._button = null;
    }

    load() {
        console.log('Búsqueda por elemento cargado correctamente.');
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
        console.log('Búsqueda por elemento no disponible');
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
        this._button = new Autodesk.Viewing.UI.Button('SummaryIconButton');
        this._button.onClick = (ev) => {
            this.viewer.clearSelection();
            this.viewer.clearThemingColors();
            /* console.log($('td:nth-child(1)')[0].innerHTML); */
            /* var tabla = document.getElementsByClassName('htCore'); */
            var tablas = document.getElementsByTagName("table");
            var tabla = tablas[8];
            let mi_visor = this.viewer;
            
            /* var filas = tabla.childNodes[1].childNodes;
            var ids = [];
            filas.forEach(element => {
            console.log(parseInt(ids));
                ids.push(parseInt(element.firstChild.innerHTML));
                
            });
            console.log(ids);
            ids.forEach(element => {
                mi_visor.setThemingColor(element,new THREE.Vector4(0, 2, 0, 1));
            });
            this.viewer.isolate(ids); */
            var filas = tabla.childNodes[1].childNodes;
            var ids = [];
            filas.forEach(element => {
            console.log(parseInt(ids));
                ids.push(parseInt(element.firstChild.innerHTML));
                
            });
            console.log(ids);
            ids.forEach(element => {
                mi_visor.setThemingColor(element,new THREE.Vector4(0, 2, 0, 1));
            });
            this.viewer.isolate(ids);
        };
        this._button.setToolTip('Buscar elemento');
        this._button.addClass('modelSummaryIcon');
        this._group.addControl(this._button);
    }
}

Autodesk.Viewing.theExtensionManager.registerExtension('SummaryIcon', SummaryIcon);