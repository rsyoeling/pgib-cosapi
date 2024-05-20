var viewer;

function launchViewer(urn) {
  var options = {
    env: 'AutodeskProduction',
    getAccessToken: function (onGetAccessToken) {
      var token = localStorage.getItem("token");
      onGetAccessToken(token, 3600);}
  };

  Autodesk.Viewing.Initializer(options, () => {
    viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById('viewer'), { extensions: [ 'Autodesk.DocumentBrowser','HandleSelectionExtension1','MyAwesomeExtension','SummaryIcon'] });
    //viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById('forgeViewer'), { extensions: [ 'Autodesk.DocumentBrowser','MyAwesomeExtension','HandleSelectionExtension1','SummaryIcon'] });
    viewer.start();
    var documentId = 'urn:' + urn;
    Autodesk.Viewing.Document.load(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);
  });
}

function onDocumentLoadSuccess(doc) {
  var viewables = doc.getRoot().getDefaultGeometry();
  viewer.loadDocumentNode(doc, viewables).then(i => {
    // documented loaded, any action?
  });
}

function onDocumentLoadFailure(viewerErrorCode, viewerErrorMsg) {
  console.error('onDocumentLoadFailure() - errorCode:' + viewerErrorCode + '\n- errorMessage:' + viewerErrorMsg);
}

function getForgeToken(callback) {
  fetch('/api/forge/oauth/token').then(res => {
    res.json().then(data => {
      callback(data.access_token, data.expires_in);
    });
  });
}
