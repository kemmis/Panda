import { Component, OnDestroy, AfterViewInit, EventEmitter, Input, Output, ElementRef, Inject } from '@angular/core';
import { ControlValueAccessor } from '@angular/forms';
declare var $: any;

@Component({
    selector: 'tiny-mce-profile',
    template: `<textarea id="{{elementId}}"></textarea>`
})
export class TinyMceProfileComponent implements AfterViewInit, OnDestroy {

    constructor(private _elementRef: ElementRef, @Inject('BASE_URL') private originUrl: string) { }
    @Input() elementId: String;

    editor: any;
    propagateChange = (_: any) => { };

    get content(): string {
        return this.editor.getContent();
    }
    set content(content: string) {
        this.editor.setContent(content);
    }

    ngAfterViewInit() {
        tinymce.init({
            selector: '#' + this.elementId,
            height: 100,
            skin_url: '../tinymce/skins/lightgray',
            setup: (editor: any) => {
                this.editor = editor;
            },
            branding: false,
            menubar: false,
            plugins: ['code'],
            toolbar: 'undo redo | bold italic | code',
            statusbar: false
        });
    }

    ngOnDestroy() {
        tinymce.remove(this.editor);
    }
}