// Complete component system with props

function BlogPost({ title, author, content, date = 'Today' }) {
  return `
    <article className="blog-post">
      <h2>${title}</h2>
      <div className="meta">
        <span className="author">By ${author}</span>
        <span className="date">${date}</span>
      </div>
      <p className="content">${content}</p>
    </article>
  `;
}

function AuthorBio({ name, bio, avatar = 'default-avatar.png' }) {
  return `
    <div className="author-bio">
      <img src="${avatar}" alt="${name}" className="avatar" />
      <h3>About ${name}</h3>
      <p>${bio}</p>
    </div>
  `;
}

function CommentSection({ comments = [] }) {
  if (comments.length === 0) {
    return '<p>No comments yet.</p>';
  }
  
  return `
    <div className="comments">
      <h3>${comments.length} Comment${comments.length !== 1 ? 's' : ''}</h3>
      ${comments.map(c => `
        <div className="comment">
          <strong>${c.author}:</strong> ${c.text}
        </div>
      `).join('')}
    </div>
  `;
}

function Blog() {
  let posts = [
    {
      title: 'Getting Started with React',
      author: 'Alice Johnson',
      content: 'React makes building user interfaces simple and enjoyable!',
      date: 'Jan 15, 2025'
    },
    {
      title: 'Understanding Props and Components',
      author: 'Alice Johnson',
      content: 'Props are the way we pass data between components.',
      date: 'Jan 16, 2025'
    }
  ];
  
  let comments = [
    { author: 'Bob', text: 'Great article!' },
    { author: 'Charlie', text: 'Very helpful, thanks!' }
  ];
  
  return `
    <div className="blog">
      <header>
        <h1>My React Blog</h1>
      </header>
      
      <main>
        ${posts.map(post => BlogPost(post)).join('\n')}
      </main>
      
      <aside>
        ${AuthorBio({
          name: 'Alice Johnson',
          bio: 'Full-stack developer passionate about React and modern web development.',
          avatar: 'alice-avatar.jpg'
        })}
      </aside>
      
      ${CommentSection({ comments })}
    </div>
  `;
}

console.log('=== Complete Blog Application ===\n');
console.log(Blog());

// Demonstrate component reusability
console.log('\n=== Reusability Demo ===\n');
console.log('Creating 3 blog posts with same component:');

for (let i = 1; i <= 3; i++) {
  console.log(BlogPost({
    title: `Post ${i}`,
    author: 'Demo Author',
    content: `Content for post number ${i}`
  }));
}