import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputUrlsViewComponent } from './input-urls-view.component';

describe('InputUrlsViewComponent', () => {
  let component: InputUrlsViewComponent;
  let fixture: ComponentFixture<InputUrlsViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InputUrlsViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InputUrlsViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
