import { ComponentFixture, TestBed } from '@angular/core/testing';

import TableUrlsViewComponent from './table-urls-view.component';

describe('TableUrlsViewComponent', () => {
  let component: TableUrlsViewComponent;
  let fixture: ComponentFixture<TableUrlsViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TableUrlsViewComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TableUrlsViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
