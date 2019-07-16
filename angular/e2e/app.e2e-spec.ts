import { MonitorVTemplatePage } from './app.po';

describe('MonitorV App', function() {
  let page: MonitorVTemplatePage;

  beforeEach(() => {
    page = new MonitorVTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
