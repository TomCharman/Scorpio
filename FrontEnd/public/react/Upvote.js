'use strict';


const e = React.createElement;

export default class UpvoteLink extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    const { comment: { id, voteCount }, onVote } = this.props
    const countString = voteCount > 0 ? ` (${voteCount})` : ''

    return e(
      'a',
      { onClick: () => onVote(id) },
      `▲ Upvote${countString}`
    );
  }
}