let editor;

async function ensureMonacoLoaded() {

    if (window.__monacoReady)
        return window.__monacoReady;

    window.__monacoReady = new Promise(resolve => {
        require(["vs/editor/editor.main"], () => resolve());
    });

    return window.__monacoReady;
}

export async function createEditor(elementId, initialValue, language) {

    await ensureMonacoLoaded();

    const container = document.getElementById(elementId);

    if (!container)
        return;

    if (editor) {
        editor.dispose();
        editor = null;
    }

    editor = monaco.editor.create(container,{
        value: initialValue ?? "",
        language: language ?? "plaintext",
        theme: "vs-dark",
        automaticLayout: true,
        minimap:{
            enabled:false
        },
        fontSize:14
    });
}

export function setEditorValue(value){
    if(editor){
        editor.setValue(value ?? "");
    }
}

export function setLanguage(language){
    if(editor){
        monaco.editor.setModelLanguage(
            editor.getModel(),
            language
        );
    }
}

export function getEditorValue(){
    return editor ? editor.getValue() : "";
}