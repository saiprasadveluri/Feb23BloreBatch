import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[appHighlight]'
})
export class HighlightDirective implements OnInit {
  @Input('appHighlight') rolestring:string='';
  constructor(private elem: ElementRef) 
  { 

  }
  ngOnInit(): void {
    
    if(this.elem.nativeElement!=null)
    {
      switch(this.rolestring.toUpperCase())
      {
        case 'OWNER':
          console.log('from option: '+this.rolestring);
          this.elem.nativeElement.style.backgroundColor='gray';
          break;

      }
    }
  }

}
