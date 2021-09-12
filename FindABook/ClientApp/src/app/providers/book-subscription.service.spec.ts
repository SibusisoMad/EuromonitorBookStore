import { TestBed } from '@angular/core/testing';

import { BookSubscriptionService } from './book-subscription.service';

describe('BookSubscriptionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BookSubscriptionService = TestBed.get(BookSubscriptionService);
    expect(service).toBeTruthy();
  });
});
