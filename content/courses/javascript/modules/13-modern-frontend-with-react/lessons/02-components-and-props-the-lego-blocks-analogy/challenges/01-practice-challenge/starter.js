// Component functions

function BlogPost({ title, author, content }) {
  return `
    <article className="blog-post">
      <h2>${title}</h2>
      <p className="author">By ${author}</p>
      <p>${content}</p>
    </article>
  `;
}

function AuthorBio({ name, bio }) {
  return `
    <div className="author-bio">
      <h3>About ${name}</h3>
      <p>${bio}</p>
    </div>
  `;
}

function Blog() {
  return `
    <div className="blog">
      <h1>My Blog</h1>
      ${BlogPost({
        title: 'Learning React',
        author: 'Alice',
        content: 'React is amazing for building UIs!'
      })}
      ${BlogPost({
        title: 'Understanding Props',
        author: 'Alice',
        content: 'Props make components reusable.'
      })}
      ${AuthorBio({
        name: 'Alice',
        bio: 'Web developer and React enthusiast.'
      })}
    </div>
  `;
}

console.log(Blog());