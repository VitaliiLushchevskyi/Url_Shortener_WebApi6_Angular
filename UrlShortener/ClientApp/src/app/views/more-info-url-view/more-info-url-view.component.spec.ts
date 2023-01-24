import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoreInfoUrlViewComponent } from './more-info-url-view.component';

describe('MoreInfoUrlViewComponent', () => {
  let component: MoreInfoUrlViewComponent;
  let fixture: ComponentFixture<MoreInfoUrlViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MoreInfoUrlViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MoreInfoUrlViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
