import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RawMaterailsComponent } from './raw-materails.component';

describe('RawMaterailsComponent', () => {
  let component: RawMaterailsComponent;
  let fixture: ComponentFixture<RawMaterailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RawMaterailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RawMaterailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
